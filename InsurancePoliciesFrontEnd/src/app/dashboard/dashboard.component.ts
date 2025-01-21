import { Component, OnInit, inject } from '@angular/core';
import { Task } from '../Model/Task';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { TaskService } from '../Services/task.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit{
  showCreateTaskForm: boolean = false;
  showTaskDetails: boolean = false;
  http: HttpClient = inject(HttpClient)
  displayedPolicies:Task[]=[]
  AllPolicies: Task[]= [];
  taskService: TaskService = inject(TaskService);
  currentPolicyId: string = '';
  isLoading: boolean = false;

  currentPolicy: Task | null = null;

  errorMessage: string | null = null;
  successMessage:string|null = null;
  displayMessage:string|null = null;

  editMode: boolean = false;
  selectedPolicy: Task;

  errorSub: Subscription

  ngOnInit(){
    this.fetchAllPolicies();
    this.errorSub = this.taskService.errorSubject.subscribe({next: (httpError) => {
      this.setErrorMessage(httpError);
    }})
  }

  ngOnDestroy(){
    this.errorSub.unsubscribe();
  }

  OpenCreateTaskForm(){
    this.showCreateTaskForm = true;
    this.editMode = false;
    // this.selectedPolicy = {name: '', desc: ''}
  }

  showCurrentTaskDetails(id: string | undefined){
    this.showTaskDetails = true;
    this.taskService.getTaskDetails(id).subscribe({next: (data: Task) => {
      this.currentPolicy = data;
    }});
  }

  CloseTaskDetails(){
    this.showTaskDetails = false;
  }

  CloseCreateTaskForm(){
    this.showCreateTaskForm = false;
  }

  CreateOrUpdateTask(data: Task){
    if(!this.editMode)
      this.taskService.CreateTask(data).subscribe({
    next:(resp)=>{
      console.log(resp)
      this.fetchAllPolicies();
      this.successMessage= 'Policy Added Successfully'
      this.displayMessage=this.successMessage;
      this.timeout()
    },
        error: (err) => {
          this.taskService.errorSubject.next(err);
      }});
    else {
      this.taskService.UpdateTask(this.currentPolicyId, data).subscribe({
        next:(resp)=>{
            console.log(resp);
            this.fetchAllPolicies();
            this.successMessage= 'Policy Updated Successfully'
            this.displayMessage=this.successMessage
            this.timeout()
        },
        error: (err) => {
        this.taskService.errorSubject.next(err);
        this.setErrorMessage(err);
    }});;
    }
      
  }

    updateErrorMessage(error:string){
      this.setErrorMessage(error)
    }

  FetchAllPoliciesClicked(){
    this.fetchAllPolicies()
  }

  private fetchAllPolicies(){
    this.isLoading = true;
    this.taskService.GetAllPolicies().subscribe({
      next: (tasks) => {
      this.isLoading = false;
      this.AllPolicies = tasks;
      this.displayedPolicies=this.AllPolicies;
    }, error: (error) => {
      this.setErrorMessage(error);
      this.isLoading = false;
    }})
  }

  private setErrorMessage(err){
    if(err.error?.error === 'Permission denied'){
      this.errorMessage = 'You do not have permisssion to perform this action';
    }
    else{
      this.errorMessage = err.message? err.message: err;
      this.displayMessage=this.errorMessage;
    }

    this.timeout()
  }

  timeout(){
    setTimeout(() => {
      this.displayMessage = null;
      this.successMessage=null;
      this.errorMessage=null;
    }, 3000);
  }

  DeleteTask(id: string | undefined){
    this.taskService.DeletePolicy(id).subscribe({
      next:(res)=>{
      console.log(res);
      this.fetchAllPolicies()
      this.successMessage= 'Policy deleted Successfully'
            this.displayMessage=this.successMessage

      },
      error: (err) => {
      // this.errorSubject.next(err);
  }});;
  }

  OnEditPolicyClicked(id: string | undefined){
    console.log(id);
    this.currentPolicyId = id;
    
    //OPEN EDIT TASK FORM
    this.showCreateTaskForm = true;
    this.editMode = true;
    this.selectedPolicy = this.AllPolicies.find((policy) => {return policy.id === id})
    console.log(this.selectedPolicy);
    this.taskService.editTask(this.selectedPolicy)
    
  }

  onSearchClicked(data:any){
    console.log(data)
    if(!data.data){
      this.displayedPolicies=this.AllPolicies
    } else {
      this.displayedPolicies= this.AllPolicies.filter((task)=>(task.title).toLowerCase().includes(data.target.value.toLowerCase()))

    }
    
  }
}
