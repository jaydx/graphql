import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
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

    const getRecord =  gql('{characters(search:"$searchPhrase", type: "$type") { appearsIn, id, name, type, age }}'
    .replace('$searchPhrase', this.searchPhrase)
    .replace('$type', this.selectedType));

    this.apollo
      .watchQuery({
        query: getRecord
      })
      .valueChanges.subscribe(result => {
        this.searchedCharacters = result.data && result.data['characters'];
        this.loading = result.loading;
      });
  }

  saveCharacterChange(character: Character){
    console.log(character);

    let mut = null;
    let vars = null;
    if (character.type === 'Droid')
    {
      mut =  gql(`
        mutation ($droid:DroidInput!){ updateDroid(droid: $droid) { id name } }
      `);

      vars = gql(`
      {"droid": {
        "id":"$id",
        "name":"$name",
        "friends":$friends,
        "primaryFunction":$primaryfunction,
        "age":$age
      }}`
      .replace('$id', character.id)
      .replace('$name', character.name)
      .replace('$age', character.age.toString())
//      .replace('$friends', character.friends.toString())
      );
    }

    this.apollo
      .mutate({
        variables: vars,
        mutation: mut
      })
      .subscribe(result => {
        console.log(result);
//        this. = result.data;
      });
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
  id: string;
  episodes: Array<number>;
  age: number;
  friends: Array<string>;
  type: string;
}