import { ShelfalyticsUIPage } from './app.po';

describe('shelfalytics-ui App', function() {
  let page: ShelfalyticsUIPage;

  beforeEach(() => {
    page = new ShelfalyticsUIPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
