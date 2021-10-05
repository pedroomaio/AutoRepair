using AutoRepair.Data.Entities;
using AutoRepair.Models;

namespace AutoRepair.Helpers
{
    public interface IConverterHelper
    {
        Car ToProduct(CarsViewModel model, bool isNew);

        CarsViewModel ToProductViewModel(Car car);
        //public AutoPiece ToAutoPiece(AutoPiecesViewModel models, bool isNew);

        //public AutoPiecesViewModel ToAutoPieceViewModel(AutoPiece autoPiece);

    }
}
