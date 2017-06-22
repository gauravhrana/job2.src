IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunDelete')
BEGIN
	PRINT 'Dropping Procedure RenumberRunDelete'
	DROP  Procedure RenumberRunDelete
END
GO

PRINT 'Creating Procedure RenumberRunDelete'
GO
/******************************************************************************
**		File: 
**		Name: RenumberRunDelete
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
CREATE Procedure dbo.RenumberRunDelete
(
	@RenumberRunId 		INT						
	
)
AS
BEGIN

	DELETE	 dbo.RenumberRun
	WHERE	 RenumberRunId = @RenumberRunId

	
END
GO
