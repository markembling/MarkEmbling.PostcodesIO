MarkEmbling.PostcodesIO
=======================

.NET library for interacting with the excellent [Postcodes.io][1] service.

### Usage

    var client = new PostcodesIOClient();
	var result = client.Lookup("GU1 1AA");
    // result contains properties such as Latitude, Longitude, Region, County and so on...

Check out the integration tests (`MarkEmbling.PostcodesIO.Tests`) for further examples.

----------

This library is still a work-in-progress. More examples and documentation will come soon, along with the missing API methods.

In the meantime, the package is [available on NuGet][2]:

    PM> Install-Package MarkEmbling.PostcodesIO

Be aware: the API may fluctuate until it hits v1.0. But I think it's already going down the right path, so we're probably fine for the most part. But as always, use at your own risk.

### Contributions

Feel free to make contributions via a pull request. Please keep the tests current (and add, if necessary).


[1]: http://postcodes.io/
[2]: https://www.nuget.org/packages/MarkEmbling.PostcodesIO/
