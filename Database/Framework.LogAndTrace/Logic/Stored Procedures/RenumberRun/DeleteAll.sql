IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDeleteAll')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDeleteAll'
	DROP  Procedure RenumberRunDeleteAll
END
GO

PRINT 'Creating Procedure RenumberRunDeleteAll'
GO
/******************************************************************************
**		File: 
**		Name: RenumberRunDeleteAll
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
CREATE Procedure dbo.RenumberRunDeleteAll
(
	@RenumberRunId 		INT						
	
)
AS
BEGIN

	Delete from	 dbo.RenumberRun

	
END
GO
