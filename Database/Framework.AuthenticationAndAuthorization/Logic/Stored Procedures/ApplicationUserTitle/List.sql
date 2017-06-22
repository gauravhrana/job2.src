IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleList')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserTitleList'
	DROP  Procedure  dbo.ApplicationUserTitleList
END
GO

PRINT 'Creating Procedure ApplicationUserTitleList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleList
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

CREATE Procedure dbo.ApplicationUserTitleList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationUserTitle'
)
AS
BEGIN

	SELECT		a.*
	FROM		dbo.ApplicationUserTitle a
	ORDER BY	a.SortOrder	ASC
		,		a.Name			ASC
		,		a.ApplicationUserTitleId	ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
		
GO