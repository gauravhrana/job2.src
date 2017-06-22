IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleList')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleList'
	DROP  Procedure  dbo.DateRangeTitleList
END
GO

PRINT 'Creating Procedure DateRangeTitleList'
GO

/******************************************************************************
**		File: 
**		Name: DateRangeTitleList
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

CREATE Procedure dbo.DateRangeTitleList
(
		@ApplicationId			INT
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DateRangeTitle'
)
AS
BEGIN

	SELECT	a.DateRangeTitleId	
		,   a.ApplicationId   
		,	a.Name		  	
		,	a.[Description]	   
		,	a.SortOrder
	 FROM	dbo.DateRangeTitle a
	 WHERE	a.ApplicationId		=	@ApplicationId

	ORDER BY a.SortOrder			ASC
		,	 a.DateRangeTitleId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO