IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationModeList')
BEGIN
	PRINT 'Dropping Procedure ApplicationModeList'
	DROP  Procedure  dbo.ApplicationModeList
END
GO

PRINT 'Creating Procedure ApplicationModeList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationModeList
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

CREATE Procedure dbo.ApplicationModeList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationMode'
)
AS
BEGIN

	SELECT	a.ApplicationModeId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.Description	   
		,	a.SortOrder
	 FROM	dbo.ApplicationMode a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.ApplicationModeId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO