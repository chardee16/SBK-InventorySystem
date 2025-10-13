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
	,ClientID
	,ClientName
)
VALUES
(
	@_BranchCode
	,@_TransactionCode
	,@ControlNo
	,@_TransYear
	,'@_TransactionDate'
	,'@_Explanation'
	,GETDATE()
	,@_UserID
	,0
	,@_ClientID
	,'@_ClientName'
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
	,UnitID
	,ClientID
)
VALUES
@_TransactionDT
;


INSERT INTO tblTransactionCheck
(
	TransactionCode
    ,CTLNo
    ,TransYear
    ,TenderedAmount
    ,ChangeAmount
    ,TaxAmount
    ,TotalAmount
    ,UPDTag
)
VALUES
(
	@_TransactionCode
	,@ControlNo
	,@_TransYear
	,@_TenderedAmount
	,@_ChangeAmount
	,@_TaxAmount
	,@_TotalAmount
	,1
);




DROP TABLE #TEMPCtlNo;
DROP TABLE #TEMPID;