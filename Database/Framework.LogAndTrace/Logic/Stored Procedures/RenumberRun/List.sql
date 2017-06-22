IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RenumberRunList')
BEGIN
	PRINT 'Dropping Procedure RenumberRunList'
	DROP  Procedure  dbo.RenumberRunList
END
GO

PRINT 'Creating Procedure RenumberRunList'
GO

/******************************************************************************
**		File: 
**		Name: RenumberRunList
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
**     ----------					   ---------
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

CREATE Procedure dbo.RenumberRunList
(
	@SystemEntityType		VARCHAR(50)	= 'RenumberRun'
)
AS
BEGIN

	SELECT  RenumberRunId			
		,	ParentSP				
		,   EntityName		  		
				
	FROM	dbo.RenumberRun 
	ORDER BY RenumberRunId			ASC

	

END
		
GO