SELECT COALESCE(Max(CTLNo),0) + 1 as CTLNo 
INTO #TEMPCtlNo
FROM tblTransactionDelivery 
Where TransactionCode = @_TransactionCode AND TransYear = @_TransYear;


DECLARE 
@ControlNo bigint
SET @ControlNo = (SELECT CTLNo FROM #TEMPCtlNo)
;

INSERT INTO tblTransactionDelivery
(
	BranchCode
	,TransactionCode
	,CTLNo
	,TransYear
	,ItemCode
	,Quantity
	,Price
	,Discount
	,TotalDiscount
	,Amt
	,EncodedBy
	,TransactionDate
	,UPDTag
	,ClientID
	,DeliveryID

)
VALUES
@_TransactionDL
;


DROP TABLE #TEMPCtlNo;