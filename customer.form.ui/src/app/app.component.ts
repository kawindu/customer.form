import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CustomerService } from '../app/services/customer.service';
import { Customer } from '../app/models/Customer';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'Customer Form';
  customerform: FormGroup;
  response = '';

  constructor(
    private formBuilder: FormBuilder,
    private customerService: CustomerService
  ) {
    this.customerform = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]]
    });
  }

  onSubmit(formData: Customer) {
    if (this.customerform.invalid)
      return
      this.customerService.saveCustomer(this.customerform.value).subscribe((data: Customer) => {
        this.response = data.firstName + ' sucessfully added.';
    });
  }
}