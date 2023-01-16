import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { Word } from '../vocabulary/word';

@Component({
  selector: 'add-word',
  templateUrl: './add-word.component.html',
  styleUrls: ['./add-word.component.css']
})
export class AddWordComponent implements OnInit {

  partsOfSpeech!: string[];
  title?: string;
  id?: number;
  word?: Word;

  form!: FormGroup;

  constructor(
    private http: HttpClient,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      word: new FormControl('', Validators.required),
      partOfSpeech: new FormControl('', Validators.required),
      translation: new FormControl('', Validators.required),
      description: new FormControl(''),
    });

    this.loadData();
  }

  loadData() {
    //Gets id parameter from url template if it's there
    var idParam = this.activatedRoute.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : 0;

    this.http.get<string[]>(environment.baseUrl + 'api/Vocabulary/GetPartsOfSpeech')
      .subscribe(result => this.partsOfSpeech = result, error => console.error(error));

    //If id exists it means that user wants to edit a word
    if (this.id) {
      var params = new HttpParams()
        .set("id", this.id);

      this.http
        .get<Word>(environment.baseUrl + 'api/Vocabulary/GetWordById/', { params })
        .subscribe(result => {
          this.word = result;
          this.form.patchValue(this.word);
          this.title = "Edit '" + this.word.word + "' word";
        });
    }
    //If id doesn't exist than user wants add new word
    else {
      this.title = "Add new word";
    }
  }

  onSubmit() {
    //Create new word object
    var word = this.id ? this.word : <Word>{};
    if (word) {
      word.word = this.form.controls['word'].value;
      word.partOfSpeech = this.form.controls['partOfSpeech'].value;
      word.translation = this.form.controls['translation'].value;
      word.description = this.form.controls['description'].value;
    }

    //Edit word mode
    if (this.id) {
      this.http
        .put<any>(environment.baseUrl + 'api/Vocabulary/UpdateWord', word)
        .subscribe(result => {
          //Redirect user to Vocabulary page
          this.router.navigate(['/vocabulary']);
        }, error => console.log(error));
    }
    //Add new word mode
    else {
      this.http
        .post<Word>(environment.baseUrl + 'api/Vocabulary/AddNewWord', word)
        .subscribe(newWordId => {
          console.log("New word Id is: " + newWordId);
          //Redirect user to Vocabulary page
          this.router.navigate(['/vocabulary']);
        }, error => console.error(error));
    }
  }

  deleteWord()
  {
    if (this.id) {
      var params = new HttpParams()
        .set("id", this.id);

      this.http
        .delete<any>(environment.baseUrl + 'api/Vocabulary/DeleteWord', { params })
        .subscribe(result => {
          //Redirect user to Vocabulary page
          this.router.navigate(['/vocabulary']);
        }, error => console.error(error));
    }
  }
}
