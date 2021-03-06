﻿#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
#endregion

namespace RoomEditorApp
{
  [Transaction( TransactionMode.ReadOnly )]
  class CmdUploadAllRooms : IExternalCommand
  {
    public Result Execute(
      ExternalCommandData commandData,
      ref string message,
      ElementSet elements )
    {
      UIApplication uiapp = commandData.Application;
      UIDocument uidoc = uiapp.ActiveUIDocument;
      Document doc = uidoc.Document;

      FilteredElementCollector rooms
        = new FilteredElementCollector( doc )
          .OfClass( typeof( SpatialElement ) )
          .OfCategory( BuiltInCategory.OST_Rooms );

      foreach( Room room in rooms )
      {
        CmdUploadRooms.UploadRoom( doc, room );
      }

      DbUpdater.SetLastSequence();

      return Result.Succeeded;
    }
  }
}
