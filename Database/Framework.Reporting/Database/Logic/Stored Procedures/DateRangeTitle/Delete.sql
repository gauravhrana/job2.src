IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DateRangeTitleDelete')
BEGIN
	PRINT 'Dropping Procedure DateRangeTitleDelete'
	DROP  Procedure DateRangeTitleDelete
END
GO

PRINT 'Creating Procedure DateRangeTitleDelete'
GO
/******************************************************************************
**		File: 
**		Name: DateRangeTitleDelete
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.DateRangeTitleDelete
(
		@DateRangeTitleId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DateRangeTitle'
)
AS
BEGIN

	DELETE	 dbo.DateRangeTitle
	WHERE	 DateRangeTitleId = @DateRangeTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DateRangeTitle'
		,	@EntityKey				= @DateRangeTitleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
