IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ModuleList')
BEGIN
	PRINT 'Dropping Procedure ModuleList'
	DROP  Procedure  dbo.ModuleList
END
GO

PRINT 'Creating Procedure ModuleList'
GO

/******************************************************************************
**		File: 
**		Name: ModuleList
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ModuleList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Module'
)
AS
BEGIN

	SELECT	a.ModuleId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.Module a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.ModuleId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO