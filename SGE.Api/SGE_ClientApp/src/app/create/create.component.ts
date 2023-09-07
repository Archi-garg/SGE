import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { CrudServiceService } from '../service/crud-service.service';
@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent {
  selectedFile: any;
  imageUrl:string = "";
  uploadForm : any;

  constructor(private fb: FormBuilder, private crudService: CrudServiceService){}

  get ItemName(){
    return this.newProduct.controls['Name'];
  }
  get itemImage(){
    return this.newProduct.controls['Image'];
  }
  get itemDesc(){
    return this.newProduct.controls['Description'];
  }
  get itemCategory(){
    return this.newProduct.controls['Category'];
  }
  get itemQuantity(){
    return this.newProduct.controls['Quantity'];
  }
  get itemPrice(){
    return this.newProduct.controls['Price'];
  }

  newProduct = this.fb.group({
    Name: ['',[Validators.required]],
    Image: ['', [Validators.required]],
    Description: ['', [Validators.required]],
    Category: ['', [Validators.required]],
    Quantity: ['', [Validators.required, Validators.min(1)]],
    Price:['', [Validators.required, Validators.min(1)]]
  })

  onFileSelected(event: any) {
    const formData: FormData = new FormData();
    formData.append("Image", event.target.files[0], event.target.files[0].name );
    this.uploadForm = formData
  }

  AddItem(value: any) {
    this.uploadForm.append("Name", value.Name);
    this.uploadForm.append("Description", value.Description);
    this.uploadForm.append("Category", value.Category);
    this.uploadForm.append("Quantity", value.Quantity);
    this.uploadForm.append("Price", value.Price);
    this.crudService.createNewProduct(this.uploadForm).subscribe((res)=>{
      if(res.success){console.log("successfully created")}
      else{console.log(res.error)}
    })
  }
  
}
