IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepDetails')
BEGIN
	PRINT 'Dropping Procedure SleepDetails'
	DROP  Procedure SleepDetails
END
GO

PRINT 'Creating Procedure SleepDetails'
GO


/******************************************************************************
**		File: 
**		Name: SleepDetails
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
**     ----------							-----------
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

CREATE Procedure dbo.SleepDetails
(
		@SleepId			INT					
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50) = 'Sleep'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SleepId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	SleepId
		,	StudentId
		,	Date
		,	NapStart
		,	NapEnd	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	Sleep 
	WHERE	SleepId		  = @SleepId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SleepId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   