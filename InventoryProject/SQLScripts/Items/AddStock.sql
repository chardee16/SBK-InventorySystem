SELECT COALESCE(Max(CTLNo),0) + 1 as CTLNo 
INTO #TEMPCtlNo
FROM tblTransactionSummary 
Where TransactionCode = @_TransactionCode AND TransYear = @_TransYear;


DECLARE 
@ControlNo bigint
;
SET @ControlNo = (SELECT CTLNo FROM #TEMPCtlNo)
;


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
	,@_ItemCode
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