import { Injectable, inject } from "@angular/core";
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams, HttpEventType, HttpResponse } from '@angular/common/http';
import { Task } from "../Model/Task";
import { map, catchError, tap } from 'rxjs/operators';
import { BehaviorSubject, Observable, Subject, throwError } from 'rxjs';
import { LoggingService } from "./Logging.Service";

@Injectable({
    providedIn: 'root'
})
export class TaskService{
    http: HttpClient = inject(HttpClient);
    loggingService: LoggingService = inject(LoggingService);
    errorSubject = new Subject<HttpErrorResponse>();
    selectedTask:Subject<Task> = new BehaviorSubject(null)

    CreateTask(task: Task):Observable<HttpResponse<{name:string}>>{
        console.log(task)
       return  this.http.post<{name: string}>(
            'https://insurance-policies-5d33e-default-rtdb.firebaseio.com/policies.json', 
            task,  {observe:'response'}
            )
            .pipe(catchError((err) => {
                return throwError(() => err);
            }))
            
    }



    DeletePolicy(id: string | undefined){
       return  this.http.delete('https://insurance-policies-5d33e-default-rtdb.firebaseio.com/policies/'+id+'.json')
        .pipe(catchError((err) => {
            //Write the logic to log errors
            const errorObj = {statusCode: err.status, errorMessage: err.message, datetime: new Date()}
            this.loggingService.logError(errorObj);
            return throwError(() => err);
        }))    
    }

    GetAllPolicies(){
        return this.http.get<{[key: string]: Task}>(
            'https://insurance-policies-5d33e-default-rtdb.firebaseio.com/policies.json'
            ,{observe: 'body'}
            ).pipe(map((response) => {
                 //TRANSFORM DATA
                 let tasks = [];
                 console.log(response);
                 for(let key in response){
                   if(response.hasOwnProperty(key)){
                     tasks.push({...response[key], id: key});
                   }              
                 }
     
                 return tasks;
            }), catchError((err) => {
                //Write the logic to log errors
                const errorObj = {statusCode: err.status, errorMessage: err.message, datetime: new Date()}
                this.loggingService.logError(errorObj);
                return throwError(() => err);
            }))
    }

    UpdateTask(id: string | undefined, data: Task){ 
        return this.http.put('https://insurance-policies-5d33e-default-rtdb.firebaseio.com/policies/'+id+'.json', data)
        .pipe(catchError((err) => {
            //Write the logic to log errors
            const errorObj = {statusCode: err.status, errorMessage: err.message, datetime: new Date()}
            this.loggingService.logError(errorObj);
            return throwError(() => err);
        }))
        
    }

    editTask(task){
      this.selectedTask.next(task)
    }

    getTaskDetails(id: string | undefined){
        return this.http.get('https://angularhttpclient-f1d30-default-rtdb.firebaseio.com/tasks/'+id+'.json')
        .pipe(map((response) => {
            console.log(response)
            let task = {};
            task = {...response, id: id}
            return task;
        }))
    }
}