import { Cotal.WebClientPage } from './app.po';

describe('cotal.web-client App', () => {
  let page: Cotal.WebClientPage;

  beforeEach(() => {
    page = new Cotal.WebClientPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
