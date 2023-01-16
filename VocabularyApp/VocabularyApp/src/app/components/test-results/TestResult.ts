export interface TestResult {
  id: number;
  testType: TestTypes;
  testDate: Date;
  testedPersonName: string;
  totalQuestionsNumber: number;
  wrongAnswersNumber: number;
  wrongAnsweredQuestions: string;
}

export enum TestTypes {
  EngToRus,
  RusToEng
}

export const TestTypesToLabelMapping = {
  [TestTypes.EngToRus.toString()]: "English to Russian",
  [TestTypes.RusToEng.toString()]: "Russian to English"
}
