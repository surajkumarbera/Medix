import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import {
  Categories,
  Product,
  SubCategories,
} from 'src/app/Models/product.model';
import { CategoryService } from 'src/app/services/category-service/category.service';
import { ProductService } from 'src/app/services/product-services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit {
  file: any = null;

  constructor(
    private _productservice: ProductService,
    private _categoryservice: CategoryService,
    private fb: FormBuilder
  ) {}

  product?: Product;

  categories!: Categories[];
  subcategories!: SubCategories[];

  // this.pForm = this.fb.group({

  //   Name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)] ],
  //   lastname: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(25)] ],
  //   telnum: ['', [Validators.required, Validators.pattern] ],
  //   email: ['', [Validators.required, Validators.email] ],
  //   agree: false,
  //   contacttype: 'None',
  //   message: ''
  // });
  addedProduct = new FormGroup({
    Name: new FormControl(''),
    Price: new FormControl(''),
    Quantity: new FormControl(''),
    Vendor: new FormControl(''),
    Descriptions: new FormControl(''),
    CategoriesId: new FormControl(''),
    SubCategoriesId: new FormControl(''),
    ImageUrl: new FormControl(''),
  });

  //Image
  //img = new FormControl('');
  img: string = '';

  card_Image: string = '../../../assets/images/pill.jpg';

  setImage() {
    // this.card_Image=""+ (this.prod_Image.value).;
  }

  getFile(event: any): void {
    this.file = event.target.files[0];
  }

  AddProduct(form: Product) {
    //console.log(form.Name);
    this.product = form;
    console.log(this.product);

    let formData = new FormData();
    formData.append('name', this.product.Name || '');
    formData.append('price', this.product.Price || '');
    formData.append('quantity', this.product.Quantity || '');
    formData.append('categoriesId', this.product.CategoriesId || '');
    formData.append('subcategoriesId', this.product.SubCategoriesId || '');
    formData.append('Vendor', this.product.Vendor || '');
    formData.append('Descriptions', this.product.Descriptions || '');
    formData.append('img', this.file);

    //console.log(formData);


    this._productservice.addProduct(formData).subscribe(
      (response) => {
        console.log(response);
      },
      (err: any) => {
        console.log(err);
      }
    );

  }
  name!: string[];

  ngOnInit(): void {
    this._categoryservice.getCategories().subscribe((response) => {
      this.categories = response;
    });

    this._categoryservice
      .getSubCategories()
      .subscribe((response) => (this.subcategories = response));
  }
}
