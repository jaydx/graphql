import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  human: any;
  loading = true;
  id: string;
  types: Array<string>;

  constructor(private apollo: Apollo) {
    this.id = '1';
  }

  ngOnInit(): void {
    this.runQuery();
  }

  onSelectTypeChange(selectedType : string){
    console.log(selectedType);
  }

  runQuery() {
    this.types = ['typeA', 'typeB'];


    const getRecord =  gql('{ human (id: "$id") { id name appearsIn } }'.replace('$id', this.id));
    this.apollo
      .watchQuery({
        query: getRecord
      })
      .valueChanges.subscribe(result => {
        this.human = result.data && result.data['human'];
        this.loading = result.loading;
      });
  }
}
