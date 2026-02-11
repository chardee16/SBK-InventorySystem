DECLARE 
@id bigint
;
SET @id = (SELECT COALESCE(MAX(DeliveryID),0) + 1 FROM tblDelivery)
;


INSERT INTO tblDelivery
(
	DeliveryID,
	DeliveryDescription
)
VALUES
(
	@id,
	'@_DeliveryDescription'
)