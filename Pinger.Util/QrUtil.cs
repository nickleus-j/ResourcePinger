using QRCoder;
namespace Pinger.Util
{
    public class QrUtil
    {
        public static string GetQrSvgOfUrl(string givenUrl,int pixelsPerModule=20)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(givenUrl, QRCodeGenerator.ECCLevel.Q);
            SvgQRCode qrCode = new SvgQRCode(qrCodeData);
            string qrCodeAsSvg = qrCode.GetGraphic(pixelsPerModule);
            return qrCodeAsSvg;
        }
    }
}
