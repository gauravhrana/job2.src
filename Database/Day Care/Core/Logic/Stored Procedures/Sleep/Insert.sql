IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepInsert')
BEGIN
	PRINT 'Dropping Procedure SleepInsert'
	DROP  Procedure SleepInsert
END
GO

PRINT 'Creating Procedure SleepInsert'
GO

/******************************************************************************
**		File: 
**		Name: SleepInsert
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
CREATE Procedure dbo.SleepInsert
(
    	@SleepId			INT			 	= NULL 	OUTPUT
	,	@ApplicationId		INT		
	,	@StudentId			INT				
	,	@Date				DATETIME		
	,	@NapStart			DATETIME		
	,	@NapEnd				DATETIME		
	,	@AuditId			INT						
	,	@AuditDate			DATETIME		= NULL 
	,   @SystemEntityType	VARCHAR(50)		= 'Sleep'	
)
AS
BEGIN	
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SleepId OUTPUT, @AuditId

	INSERT INTO dbo.Sleep
	(
			SleepId
		,	ApplicationId
		,	StudentId
		,	Date
		,	NapStart
		,	NapEnd
	)
	VALUES
	(
			@SleepId
		,	@ApplicationId
		,	@StudentId
		,	@Date
		,	@NapStart
		,	@NapEnd
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SleepId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	
END
GO
