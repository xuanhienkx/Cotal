export class LoggedInUser {
  constructor(access_token: string, username: string, expiresIn: any, fullName:string,
  email:string, avatar:string,permissions:any, roles: any
    ) {
        this.access_token = access_token; 
        this.UserName = username;
        this.ExpiresIn = expiresIn;
        this.Avatar = avatar;
        this.Email = email;
        this.FullName = fullName;    
        this.Permissions = permissions;    
        this.Roles = roles;    
  }
    public Id: string;
    public access_token: string;
    public UserName: string;
    public FullName: string;
    public Email: string;
    public Avatar: string;
    public Permissions:any;
    public Roles: any;
    public ExpiresIn: any; 
  
}