export class LoggedInUser {
    constructor(access_token: string, username: string,expiresIn: number
    ) {
        this.access_token = access_token; 
        this.username = username;
        this.expiresIn=expiresIn; 
    }
    public id: string;
    public access_token: string;
    public username: string;
    public fullName: string;
    public email: string;
    public avatar: string;
    public permissions:any;
    public roles: any;
    public expiresIn: number;
}