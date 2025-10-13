SELECT COALESCE(Max(CTLNo),0) + 1 as CTLNo 
INTO #TEMPCtlNo
FROM tblTransactionSummary 
Where TransactionCode = @_TransactionCode AND TransYear = @_TransYear;

SELECT COALESCE(Max(ItemCode),0) + 1 as ItemCode 
INTO #TEMPID
FROM tblItems;



DECLARE 
@ControlNo bigint,
@ItemCode bigint
;
SET @ControlNo = (SELECT CTLNo FROM #TEMPCtlNo)
SET @ItemCode = (SELECT ItemCode FROM #TEMPID)
;


INSERT INTO tblItems
(
	ItemCode
	,ItemName
	,ItemDescription
	,ItemGenericID
	,CategoryID
	,UnitID
	,PurchasePrice
	,Price
	,MarkupValue
	,StoreShelve
	,SupplierID
	,SideEffect
	,Barcode
	,TransactionCode
	,CTLNo
	,EncodedBy
	,Value
)
VALUES
(
	@ItemCode
	,'@_ItemName'
	,'@_ItemDescription'
	,@_GenericID
	,@_CategoryID
	,@_UnitID
	,@_PurchasePrice
	,@_Price
	,@_MarkupValue
	,@_ShelfID
	,@_SupplierID
	,'@_SideEffect'
	,'@_Barcode'
	,@_TransactionCode
	,@ControlNo
	,@_UserID
	,@_Value
);




INSERT INTO tblTransactionSummary
(
	BranchCode
	,TransactionCode
	,CTLNo
	,TransYear
	,TransactionDate
	,Explanation
	,DateTimeAdded
	,PostedBy
	,ORNo
)
VALUES
(
	@_BranchCode
	,@_TransactionCode
	,@ControlNo
	,@_TransYear
	,'@_TransactionDate'
	,'Add item'
	,GETDATE()
	,@_UserID
	,0
);



INSERT INTO tblTransactionDetails
(
	BranchCode
	,TransactionCode
	,CTLNo
	,TransYear
	,ItemCode
	,Quantity
	,Price
    ,Discount
    ,DiscountAmount
	,Amt
	,EncodedBy
	,TransactionDate
	,UPDTag
	,ExpiryDate
)
VALUES
(
	@_BranchCode
	,@_TransactionCode
	,@ControlNo
	,@_TransYear
	,@ItemCode
	,@_Stock
	,@_Price
	,0
	,0
	,@_Price * @_Stock
	,@_UserID
	,'@_TransactionDate'
	,1
	,'@_ExpiryDate'
);



DROP TABLE #TEMPCtlNo;
DROP TABLE #TEMPID;