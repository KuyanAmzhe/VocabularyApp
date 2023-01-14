import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddWordComponent } from './add-word/add-word.component';
import { HomePageComponent } from './home-page/home-page.component';
import { TestResultsComponent } from './test-results/test-results.component';
import { VocabularyComponent } from './vocabulary/vocabulary.component';
import { WordTestComponent } from './word-test/word-test.component';

const routes: Routes = [
  { path: '', component: HomePageComponent, pathMatch: 'full' },
  { path: 'vocabulary', component: VocabularyComponent },
  { path: 'add-word', component: AddWordComponent },
  { path: 'add-word/:id', component: AddWordComponent },
  { path: 'tests-results', component: TestResultsComponent },
  { path: 'tests', component: WordTestComponent }
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
