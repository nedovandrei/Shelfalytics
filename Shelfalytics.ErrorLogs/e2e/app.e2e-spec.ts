import { Shelfalytics.ErrorLogsPage } from './app.po';

describe('shelfalytics.error-logs App', () => {
  let page: Shelfalytics.ErrorLogsPage;

  beforeEach(() => {
    page = new Shelfalytics.ErrorLogsPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
