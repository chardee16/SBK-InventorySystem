SELECT BranchCode
      ,ClientID
      ,ItemCode
      ,Discount
  FROM tblClientDiscount
  WHERE
  ClientID = @_ClientID and
  ItemCode = @_ItemCode
