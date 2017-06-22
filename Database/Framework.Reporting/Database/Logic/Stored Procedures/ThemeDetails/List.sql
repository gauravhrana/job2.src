IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsList')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsList'
	DROP  Procedure  dbo.ThemeDetailsList
END
GO

PRINT 'Creating Procedure ThemeDetailsList'
GO

/******************************************************************************
**		File: 
**		Name: ThemeDetailsList
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

CREATE Procedure dbo.ThemeDetailsList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetails'
)
AS
BEGIN

	SELECT	a.ThemeDetailId	
		,   a.ApplicationId   
		,	a.ThemeKeyId		  	
		,	a.Value	   
		,	a.ThemeId
		,	a.ThemeCategoryId
	 FROM	dbo.ThemeDetails a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.ThemeDetailId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO