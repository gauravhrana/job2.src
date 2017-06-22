IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleList')
BEGIN
	PRINT 'Dropping Procedure PersonTitleList'
	DROP  Procedure  dbo.PersonTitleList
END
GO

PRINT 'Creating Procedure PersonTitleList'
GO

/******************************************************************************
**		File: 
**		Name: PersonTitleList
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

CREATE Procedure dbo.PersonTitleList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'PersonTitle'
)
AS
BEGIN

	SELECT		a.*
	FROM		dbo.PersonTitle a
	ORDER BY	a.SortOrder	ASC
		,		a.Name			ASC
		,		a.PersonTitleId	ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
		
GO