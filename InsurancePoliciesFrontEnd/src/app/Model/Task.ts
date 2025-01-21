export class Task{
    constructor(
        
        
        public desc: string,
        public title: string,
        public name?: string, 
        public assignedTo?: string,
        public createdAt?: string,
        public priority?: string,
        public status?: string,
        public id?: string
    )
    {

    }
}