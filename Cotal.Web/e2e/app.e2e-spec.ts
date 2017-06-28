import { Cotal.WebPage } from './app.po';

describe('cotal.web App', () => {
  let page: Cotal.WebPage;

  beforeEach(() => {
    page = new Cotal.WebPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
