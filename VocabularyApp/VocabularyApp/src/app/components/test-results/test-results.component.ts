import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from '../../../environments/environment';
import { TestResult } from './TestResult';
import { TestTypesToLabelMapping } from './TestResult' 

@Component({
  selector: 'test-results',
  templateUrl: './test-results.component.html',
  styleUrls: ['./test-results.component.css']
})
export class TestResultsComponent implements OnInit {
  public displayedColumns: string[] = ['testType', 'testDate', 'testedPersonName', 'totalQuestionsNumber', 'wrongAnswersNumber', 'wrongAnsweredQuestions'];
  public testsResults!: MatTableDataSource<TestResult>;

  defaultPageNumber: number = 0;
  defaultPageSize: number = 10;
  public defaultSortColumn: string = 'testDate';
  public defaultSortOrder: "asc" | "desc" = "asc";
  defaultFilterColumn: string = 'testedPersonName';
  filterQuery?: string;
  public TestTypesToLabelMapping = TestTypesToLabelMapping;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageNumber;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  getData(event: PageEvent) {
    var url = environment.baseUrl + 'api/Vocabulary/TestsResults';

    var params = new HttpParams()
      .set("pageNumber", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("sortColumn", (this.sort) ? this.sort.active : this.defaultSortColumn)
      .set("sortOrder", (this.sort) ? this.sort.direction : this.defaultSortOrder);

    if (this.filterQuery) {
      params = params
        .set("filterColumn", this.defaultFilterColumn)
        .set("filterQuery", this.filterQuery);
    }

    this.http.get<any>(url, { params })
      .subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageNumber;
        this.paginator.pageSize = result.pageSize;
        this.testsResults = new MatTableDataSource<TestResult>(result.data);
      }, error => console.error(error));
  }
}
