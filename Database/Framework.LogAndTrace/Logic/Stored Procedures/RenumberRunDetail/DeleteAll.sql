IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDetailDeleteAll')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDetailDeleteAll'
	DROP  Procedure RenumberRunDetailDeleteAll
END
GO

PRINT 'Creating Procedure RenumberRunDetailDeleteAll'
GO
/******************************************************************************
**		File: 
**		Name: RenumberRunDetailDeleteAll
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.RenumberRunDetailDeleteAll

AS
BEGIN

	Delete from	 dbo.RenumberRunDetail

	
END
GO
