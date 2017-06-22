IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='NotificationRegistrarDoesExist')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarDoesExist'
	DROP  Procedure  NotificationRegistrarDoesExist
END
GO

PRINT 'Creating Procedure NotificationRegistrarDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: NotificationRegistrarDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.NotificationRegistrarDoesExist
(
		@NotificationRegistrarId				INT				= NULL
	,	@NotificationEventTypeId					INT
	,	@NotificationPublisherId					INT
	,	@ApplicationId				INT	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'NotificationRegistrar'
)
AS
BEGIN

	SELECT	a.*
	FROM	dbo.NotificationRegistrar a
	WHERE	a.NotificationEventTypeId			=	@NotificationEventTypeId
	AND		a.NotificationPublisherId			=	@NotificationPublisherId
	AND		a.ApplicationId		=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO

