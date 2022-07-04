using Moq;
using Softhouse.Converter.XmlConstructionModels;
using Softhouse.Converter.XmlDocumentModels;
using Softhouse.Shared.Metadata;

namespace Softhouse.Converter.Tests.XmlConvertingService;

public class XmlConvertingServiceTests
{
    [Theory]
    [MemberData(nameof(XmlConvertingServiceTestData.Get_ConstructXmlDocument_RowInputFormats_ReturnsXmlText), 2, MemberType = typeof(XmlConvertingServiceTestData))]
    public void ConstructXmlDocument_RowInputFormats_ReturnsXmlText(RowInputFormat[] input, Func<RowInputFormat[], Person[]> inputOutcome, string expectedResult)
    {
        //Arrange
        var pbsStub = new Mock<IPersonBuilderService>();
        pbsStub.Setup(x => x.Build(input)).Returns(inputOutcome(input));
        var xmlConvertingService = new Converter.XmlConvertingService(pbsStub.Object);

        //Act
        var xml = xmlConvertingService.Convert(input);

        //Assert
        Assert.Equal(expectedResult, xml.Text);
    }

    [Theory]
    [MemberData(nameof(XmlConvertingServiceTestData.Get_ConstructXmlDocument_ErrorRowInputFormats_ReturnsErrorStatus), 3, MemberType = typeof(XmlConvertingServiceTestData))]
    public void ConstructXmlDocument_ErrorRowInputFormats_ReturnsErrorStatus(RowInputFormat[] input, Func<RowInputFormat[], Person[]> inputOutcome, XmlConstructingErrorStatus expectedResult)
    {
        //Arrange
        var pbsStub = new Mock<IPersonBuilderService>();
        pbsStub.Setup(x => x.Build(input)).Returns(inputOutcome(input));
        var xmlConvertingService = new Converter.XmlConvertingService(pbsStub.Object);

        //Act
        var xml = xmlConvertingService.Convert(input);

        //Assert
        Assert.NotNull(xml.Error);
        Assert.Equal(expectedResult, xml.Error!.Status);
    }
}
