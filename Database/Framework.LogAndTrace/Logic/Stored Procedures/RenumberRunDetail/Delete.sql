IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDetailDelete')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDetailDelete'
	DROP  Procedure RenumberRunDetailDelete
END
GO

PRINT 'Creating Procedure RenumberRunDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: RenumberRunDetailDelete
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
CREATE Procedure dbo.RenumberRunDetailDelete
(
	@RenumberRunId 		INT						
	
)
AS
BEGIN

	DELETE	 dbo.RenumberRunDetail
	WHERE	 RenumberRunId = @RenumberRunId

	
END
GO
