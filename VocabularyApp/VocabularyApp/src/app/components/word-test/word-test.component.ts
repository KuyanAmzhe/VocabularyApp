//ATTENTION: I know this page was not made properly, 'cause, in general, I made 1 component instead of 3.
//This component is a complete mess, but I knew it was going to go this way.

import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { TestTypesToLabelMapping, TestTypes, TestResult } from '../test-results/TestResult';
import { Word } from '../vocabulary/word';

@Component({
  selector: 'word-test',
  templateUrl: './word-test.component.html',
  styleUrls: ['./word-test.component.css']
})
export class WordTestComponent implements OnInit {
  // Const object of selection list for enum
  public TestTypesToLabelMapping = TestTypesToLabelMapping;
  // TestTypes enum values array 
  public testTypes = this.testTypesArray();
  // If test setting is done it changes to true
  hideSettings = false;
  // Test settings
  testSettings: TestResult = <TestResult>{};
  // A set of words for a test question
  options!: Word[];
  // The right answer on question
  rightAnswer!: Word;
  // The answer that a user has chosen
  givenAnswer?: Word;
  // Array of wrong answered questions
  wrongAnsweredQuestions: string[] = [];
  // Test size options
  testSizes!: number[];
  currentQuestionId = 0;
  readonly optionsNumber = 4;
  // Two types of question for types of test. I know that I should not implement it this way, fuck it
  engQuestion: string = "";
  rusQuestion: string = "";
  
  form!: FormGroup;
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      testType: new FormControl('', Validators.required),
      testedPersonName: new FormControl('', Validators.required),
      totalQuestionsNumber: new FormControl('', Validators.required)
    });
    this.setTestSizesArray();
  }

  onSumbit() {
    this.agrigateData();
    this.hideSettings = true;
    // Gets the first question
    this.generateTestQuestion();
  }

  // To get enum's values use this method 
  testTypesArray() {
    let array = Object.values(TestTypes);
    return array = array.slice(array.length / 2);
  }

  agrigateData() {
    this.testSettings.testType = this.form.controls['testType'].value;
    this.testSettings.testedPersonName = this.form.controls['testedPersonName'].value;
    this.testSettings.totalQuestionsNumber = this.form.controls['totalQuestionsNumber'].value;
  }

  generateTestQuestion() {
    let params = new HttpParams()
    .set("numberOfWords", this.optionsNumber)
    .set("totalQuestionsInTest", this.testSettings.totalQuestionsNumber);

    this.http
      .get<any>(environment.baseUrl + 'api/Vocabulary/GetWordsForTest', { params })
      .subscribe(result => {
        this.options = result;
        this.generateAnswer();
        this.currentQuestionId++;
        this.engQuestion = `Question №${this.currentQuestionId}: How is the word \"${this.rightAnswer.word}\" translated into English?`;
        this.rusQuestion = `Question №${this.currentQuestionId}: How is the word \"${this.rightAnswer.translation}\" translated into Russian?`;
      }, error => console.error(error));
  }

  generateAnswer() {
    this.rightAnswer = this.options[Math.floor(Math.random() * 4)];
  }

  nextQuestion() {
    // If the answer is wrong
    if (this.givenAnswer?.id != this.rightAnswer.id)
      // Add a wrong answered question to the array
      this.wrongAnsweredQuestions.push((this.testSettings.testType == 0) ? this.engQuestion : this.rusQuestion);

    // If the question was the last one
    if (this.currentQuestionId == this.testSettings.totalQuestionsNumber) {
      document.getElementById("test")?.remove();
      // Shows test result "page"
      let testResultPage = document.getElementById("test-reslt");
      (testResultPage) ? testResultPage.hidden = false : null;
      this.finalizeTestResult();

      // Adds test result to DB
      this.http
        .post<TestResult>(environment.baseUrl + 'api/Vocabulary/AddTestResult', this.testSettings)
        .subscribe(result => {}, error => console.error(error));
    }
    else {
      // "Clears" the givenAnswer variable
      this.givenAnswer = undefined;
      // Generate new test question
      this.generateTestQuestion();
    }
  }

  setTestSizesArray() {
    this.http
      .get<number>(environment.baseUrl + 'api/Vocabulary/GetWordsCount')
      .subscribe(result => {
        let roundedWordCount = result - (result % 10);
        this.testSizes = [];
        for (let i = 10; i <= roundedWordCount; i += 10)
          this.testSizes.push(i);
      }, error => console.error(error));
  }

  finalizeTestResult() {
    this.testSettings.testDate = new Date(Date.now());
    this.testSettings.wrongAnsweredQuestions = this.wrongAnsweredQuestions.join("\n");
    this.testSettings.wrongAnswersNumber = this.wrongAnsweredQuestions.length;
  }
}