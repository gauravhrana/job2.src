IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepUpdate')
BEGIN
	PRINT 'Dropping Procedure SleepUpdate'
	DROP  Procedure  SleepUpdate
END
GO

PRINT 'Creating Procedure SleepUpdate'

GO

/******************************************************************************
**		File: 
**		Name: SleepUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.SleepUpdate
(   
		@SleepId			INT 
	,	@StudentId			INT             
	,	@Date				DATETIME		
	,	@NapStart			DATETIME		
	,	@NapEnd				DATETIME		
	,	@AuditId			INT		
	,	@AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Sleep' 
	 
)
AS
BEGIN

	UPDATE	dbo.Sleep
	SET		SleepId			=		@SleepId		
		,	StudentId		=		@StudentId
		,	Date			=		@Date
		,	NapStart		=		@NapStart
		,	NapEnd			=		@NapEnd
	WHERE	SleepId		    =       @SleepId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SleepId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		
 END
GO

