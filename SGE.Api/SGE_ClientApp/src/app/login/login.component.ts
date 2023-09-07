import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';
import { FormBuilder, Validators } from '@angular/forms';
import { PasswordValidator } from '../Shared/Confirm-Password';
import { ImageCroppedEvent, base64ToFile } from 'ngx-image-cropper';
import { v4 as uuidv4 } from 'uuid';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent{
  visible: boolean = false;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  imageFile!: any ;
  public uploadForm: FormData= new FormData();

  constructor(private authService: AuthServiceService, private fb: FormBuilder, private router:Router){}

  get userName(){
    return this.userData.controls["UserName"];
  }
  get email(){
    return this.userData.controls["Email"];
  }
  get password(){
    return this.userData.controls["Password"];
  }
  get phoneNo(){
    return this.userData.controls["PhoneNumber"];
  }
  get loginEmail(){
    return this.loginForm.controls["Email"];
  }
  get loginPassword(){
    return this.loginForm.controls["Password"];
  }

  userData = this.fb.group({
    UserName: ['', [Validators.required]],
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required, Validators.pattern("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{4,}$")]],
    ConfirmPassword: [''],
    ProfilePic:[''],
    PhoneNumber: ['', [Validators.required]]
  }, {validator: PasswordValidator})

  signup() {
    const message = document.querySelector(".message") as HTMLElement;
    if (message) {
      message.style.transform = "translateX(100%)";
      if (message.classList.contains("login")) {
        message.classList.remove("login");
      }
      message.classList.add("signup");
    }
  }

  login() {
    const message = document.querySelector(".message") as HTMLElement;
    if (message) {
      message.style.transform = "translateX(0)";
      if (message.classList.contains("signup")) {
        message.classList.remove("signup");
      }
      message.classList.add("login");
    }
  }

  onFileSelected(event:any){
    this.imageChangedEvent = event
    this.visible = true;
  }

  imageCropped(event: ImageCroppedEvent){
    this.croppedImage = event.objectUrl;
    this.imageFile = event.blob;
    const formData: FormData = new FormData();
    const uniqueName = uuidv4();
    console.log('Generated unique name:', uniqueName);
    var name = uniqueName+".png"
    formData.append("ProfilePic", this.imageFile, name);
    this.uploadForm = formData
  }

  imageLoaded(){}

  cropperReady(){}

  loadImageFailed(){
    alert("fail to load")
  }


  signupUser(value:any){
    this.uploadForm.append("UserName", value.UserName);
    this.uploadForm.append("Email", value.Email);
    this.uploadForm.append("Password", value.Password);
    this.uploadForm.append("ConfirmPassword", value.ConfirmPassword);
    this.uploadForm.append("PhoneNumber", value.PhoneNumber);
    this.authService.registerUser(this.uploadForm).subscribe((res)=>{
      if(res.success){
        console.log(res.data);
        this.login();

      }
      else console.log(res.error);
    })

  }

  loginForm = this.fb.group({
    Email: ['', [Validators.required, Validators.email]],
    Password: ['', [Validators.required]]
  })

  loginUser(value:any){
    this.authService.loginData(value).subscribe((res)=>{
      if(res.success){
        localStorage.setItem("token", res.data);
        this.router.navigate(['admin']);
      }else alert("user not exist")
    })
  }

  
}
