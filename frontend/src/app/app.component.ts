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
  searchedCharacters: Array<Character>;

  constructor(private apollo: Apollo) {
    this.id = '1';
    this.selectedType = this.anyType;
    this.searchPhrase = "";
    this.searchedCharacters = [{
        name: "bob",
        id: 1,
        episodes: ["return of the pancakes", "hallelujah 3"],
        age: 422,
        friends: ["other bob", "han solo"]
    }, {
      name: "sally",
      id: 3,
      episodes: ["tap dancing cats", "anikin throws a wobbly"],
      age: 25,
      friends: ["chewie"]
    }];
  }

  ngOnInit(): void {
    this.runTypesQuery();
  }

  onSelectTypeChange(selectedType: string) {
    console.log(`Selected Type: ${selectedType}`);
    this.selectedType = selectedType;

    this.searchCharacters();
  }

  onSearchChange(searchPhrase: string) {
    console.log(`Search: ${searchPhrase}`);
    this.searchPhrase = searchPhrase;

    this.searchCharacters();
  }

  searchCharacters() {
    console.log(`Search: ${this.searchPhrase} for ${this.selectedType} starwarstype.`);
  }

  saveCharacterChange(character: Character){
    console.log(character);
  }

  runTypesQuery() {
    const getTypes = gql('{ types }');
    this.apollo.watchQuery({
      query: getTypes
    })
      .valueChanges.subscribe(result => {
        this.types = result.data && result.data['types'];
        this.loading = result.loading;
      });

    /*
    const getRecord =  gql('{ human (id: "$id") { id name appearsIn } }'.replace('$id', this.id));
    this.apollo
      .watchQuery({
        query: getRecord
      })
      .valueChanges.subscribe(result => {
        this.human = result.data && result.data['human'];
        this.loading = result.loading;
      });
    */
  }
}

class Character {
  name: string;
  id: number;
  episodes: Array<string>;
  age: number;
  friends: Array<string>;
}