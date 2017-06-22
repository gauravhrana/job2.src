IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeList')
BEGIN
	PRINT 'Dropping Procedure FileTypeList'
	DROP  Procedure  dbo.FileTypeList
END
GO

PRINT 'Creating Procedure FileTypeList'
GO

/******************************************************************************
**		File: 
**		Name: FileTypeList
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

CREATE Procedure dbo.FileTypeList
(
		@AuditId				INT		
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'FileType'
)
AS
BEGIN

	SELECT		a.*
	FROM		dbo.FileType a
	WHERE		a.ApplicationId = @ApplicationId
	ORDER BY	a.SortOrder		ASC
		,		a.Name			ASC
		,		a.FileTypeId	ASC
		,		a.ApplicationId	ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO