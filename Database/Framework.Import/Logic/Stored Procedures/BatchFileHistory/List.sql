IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryList')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryList'
	DROP  Procedure  dbo.BatchFileHistoryList
END
GO

PRINT 'Creating Procedure BatchFileHistoryList'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileHistoryList
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

CREATE Procedure dbo.BatchFileHistoryList
(
		@AuditId				INT		
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'
)
AS
BEGIN

	SELECT	a.BatchFileHistoryId	
	    ,	a.ApplicationId								
		,	a.BatchFileId											
		,	a.BatchFileSetId										
		,	a.BatchFileStatusId										
		,	a.UpdatedDate											
		,	a.UpdatedByPersonId										
		,	b.Name			AS	'BatchFileSet'						
		,	c.Name			AS	'BatchFileStatus'					
		,	d.FirstName + ' ' + d.LastName	AS 'UpdatedByPerson'
	FROM		dbo.BatchFileHistory	a
	INNER JOIN	dbo.BatchFileSet		b		ON		a.BatchFileSetId		= b.BatchFileSetId
	INNER JOIN	dbo.BatchFileStatus		c		ON		a.BatchFileStatusId		= c.BatchFileStatusId	
	INNER JOIN	dbo.Person				d		ON		a.UpdatedByPersonId		= d.PersonId
	WHERE		ApplicationId = @ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO