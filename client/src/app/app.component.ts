import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = 'eCommerceApp';
  products: IProduct[] | undefined;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.http.get('https://localhost:5074/api/products?sort=50').subscribe(
      (response: any) => {
      this.products = response;
      console.log(response);
    }, error => {
      console.log(error);
    });
  }

}
