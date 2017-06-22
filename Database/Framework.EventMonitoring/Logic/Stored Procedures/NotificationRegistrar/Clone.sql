IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarClone')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarClone'
	DROP  Procedure NotificationRegistrarClone
END
GO

PRINT 'Creating Procedure NotificationRegistrarClone'
GO

/*********************************************************************************************
**		File: 
**		Name: NotificationRegistrarClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.NotificationRegistrarClone
(
		@NotificationRegistrarId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@NotificationEventTypeId				INT
	,	@NotificationPublisherId					INT				= NULL
	,	@Message					VARCHAR(255)	
	,	@PublishDateId								INT	= NULL				
	,	@PublishTimeId								INT	= NULL										
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NotificationRegistrar'
)
AS
BEGIN

	IF @NotificationRegistrarId IS NULL OR @NotificationRegistrarId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @NotificationRegistrarId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Message				=	Message
		,	@PublishDateId			=	PublishDateId
		,	@PublishTimeId			=	PublishTimeId
	FROM	dbo.NotificationRegistrar
	WHERE   NotificationRegistrarId		=	@NotificationRegistrarId
	ORDER BY NotificationRegistrarId

	EXEC dbo.NotificationRegistrarInsert 
			@NotificationRegistrarId			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@NotificationEventTypeId				=	@NotificationEventTypeId
		,	@NotificationPublisherId				=	@NotificationPublisherId
		,	@Message				=	@Message
		,	@PublishDateId				=	@PublishDateId
		,	@PublishTimeId				=	@PublishTimeId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
