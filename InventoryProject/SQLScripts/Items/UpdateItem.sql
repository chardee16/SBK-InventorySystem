UPDATE tblItems SET
	ItemName = '@_ItemName'
	,ItemDescription = '@_ItemDescription'
	,ItemGenericID = @_GenericID
	,CategoryID = @_CategoryID
	,PurchasePrice = @_PurchasePrice
	,Price = @_Price
	,MarkupValue = @_MarkupValue
	,StoreShelve = @_ShelfID
	,SupplierID = @_SupplierID
	,SideEffect = '@_SideEffect'
	,Barcode = '@_Barcode'
	,UnitID = @_UnitID
	,Value = @_Value
WHERE ItemCode = @_ItemCode