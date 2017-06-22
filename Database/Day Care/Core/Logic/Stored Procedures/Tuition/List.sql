IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionList')
BEGIN
	PRINT 'Dropping Procedure TuitionList'
	DROP PROCEDURE TuitionList
END
GO

PRINT 'Creating Procedure TuitionList'
GO

/******************************************************************************
**		File: 
**		Name: TuitionList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TuitionList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Tuition'
)
AS
BEGIN
		SELECT	a.*
		FROM	dbo.Tuition a
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY TuitionId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
