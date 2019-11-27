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
  anyType = "Any"; 
  selectedType: string;
  searchPhrase: string;

  constructor(private apollo: Apollo) {
    this.id = '1';
    this.selectedType = this.anyType; 
  }

  ngOnInit(): void {
    this.runQuery();
  }

  onSelectTypeChange(selectedType: string){
    console.log(`Selected Type: ${selectedType}`);
    this.selectedType = selectedType;

    this.searchCharacters();
  }

  onSearchChange(searchPhrase: string){
    console.log(`Search: ${searchPhrase}`);
    this.searchPhrase = searchPhrase;

    this.searchCharacters();
  }

  searchCharacters() {
    console.log(`Search: ${this.searchPhrase} for ${this.selectedType} starwarstype.`);
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
