import { Component, EventEmitter, Output, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Task } from 'src/app/Model/Task';
import { TaskService } from 'src/app/Services/task.service';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent {

  constructor(private taskService:TaskService){}
  @Input() isEditMode: boolean = false;

  @Input() selectedTask: Task={title:'',desc:''};

  @ViewChild('taskForm') policyForm: NgForm;

  @Output()
  CloseForm: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Output()
  EmitTaskData: EventEmitter<Task> = new EventEmitter<Task>();

  @Output()
  formValidationErrormessage:EventEmitter<string> = new EventEmitter<string>()

  title:string=''
  desc:string=''

  ngOnInit():void {
  this.taskService.selectedTask.subscribe({
    next:(task)=>{
      this.title=task.title
      this.desc=task.desc

    }
  })
  }

  ngAfterViewInit(){
    setTimeout(() => {
      this.policyForm.form?.patchValue(this.selectedTask);
    }, 0);
    
  }

  OnCloseForm(){
    this.CloseForm.emit(false);
    this.taskService.editTask({title:'',desc:''})
  }

  OnFormSubmitted(form: NgForm){
    console.log(form)
    if(form.status==="VALID"){
      this.EmitTaskData.emit(form.value);
      this.CloseForm.emit(false);
    }
    else {
      this.formValidationErrormessage.emit('Please fill all fields')
    }
   
  }
}
