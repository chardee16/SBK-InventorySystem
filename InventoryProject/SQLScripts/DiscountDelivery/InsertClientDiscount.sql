INSERT INTO tblClientDiscount
(
		BranchCode
      ,ClientID
      ,ItemCode
      ,Discount
)
VALUES
(
    @_BranchCode,
    @_ClientID,
    @_ItemCode,
    '@_Discount'
);