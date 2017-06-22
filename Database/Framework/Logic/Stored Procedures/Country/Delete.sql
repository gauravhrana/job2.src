IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryDelete')
BEGIN
	PRINT 'Dropping Procedure CountryDelete'
	DROP  Procedure CountryDelete
END
GO

PRINT 'Creating Procedure CountryDelete'
GO
/******************************************************************************
**		File: 
**		Name: CountryDelete
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
CREATE Procedure dbo.CountryDelete
(
		@CountryId 				INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Country'
)
AS
BEGIN	

	DELETE	 dbo.Country
	WHERE	 CountryId = @CountryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Country'
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
