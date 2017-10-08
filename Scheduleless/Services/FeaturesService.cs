using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using Scheduleless.Endpoints;
using Scheduleless.Models;

namespace Scheduleless.Services
{
  public class FeaturesService
  {

    private static readonly Lazy<FeaturesService> lazy = new Lazy<FeaturesService>(() => new FeaturesService());
    public static FeaturesService Instance
    {
      get
      {
        return lazy.Value;
      }
    }

    protected FeaturesEndpoint _featuresEndpoint;
    protected Features _features;

    public FeaturesService()
    {
      _featuresEndpoint = new FeaturesEndpoint();

      var response = await _featuresEndpoint.IndexAsync<Features>();
      if (response.IsSuccess)
      {
        _features = response.Result;
      }
    }

    public bool TimeClock
    {
      get { return _features.TimeClock; }
    }

    public bool Trading
    {
      get { return _features.Trading; }
    }

    public bool Wages
    {
      get { return _features.Wages; }
    }
  }
}

